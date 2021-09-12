using DE_APPLICATION_ELEANING.CurrenApp;
using DE_APPLICATION_ELEANING.QuestionApp.Dto;
using DE_DB_ELEANING.AFModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
namespace DE_APPLICATION_ELEANING.QuestionApp
{
    public class QuestionApplication : AsyncCrudAppService
        <SearchQuestionDto, Question, Question,
        Guid, List<Question>, bool,
        bool, Question, bool>, IQuestionApplication
    {
        public readonly Entities _Db_Context;

        public QuestionApplication()
        {
            if (_Db_Context == null)
            {
                _Db_Context = new Entities();
            }
        }

        public async Task<int> Appvore(Guid Id, bool status)
        {
            try
            {
                var _result = _Db_Context.Questions.Find(Id);
                _result.Status = status;
                _Db_Context.SaveChanges();
                return await Task.FromResult(1);
            }
            catch
            {
                return await Task.FromResult(0);
            }
        }

        public async Task<int> AppvoreListAsync(string[] List, bool status)
        {
            int count = 0;
            try
            {
                for (int i = 1; i < List.Length; i++)
                {
                    var del = await this.Appvore(Guid.Parse(List[i]), status);
                    count += del;
                }
                return await Task.FromResult(count);
            }
            catch
            {
                return await Task.FromResult(count);
            }
        }

        public override async Task<bool> CreateAsync(Question input)
        {
            try
            {

                input.Answers = null;
                _Db_Context.Questions.Add(input);
                _Db_Context.SaveChanges();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> CreateVocabularyResult(VocabularyResult input)
        {
            try
            {
                input.Id = Guid.NewGuid();
                _Db_Context.VocabularyResults.Add(input);
                _Db_Context.SaveChanges();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return await Task.FromResult(false);
            }
        }

        public async Task<int> DeleleListAsync(string[] List)
        {
            int count = 0;
            try
            {
                for (int i = 1; i < List.Length; i++)
                {
                    var del = await this.DeleteAsync(Guid.Parse(List[i]));
                    if (del == true)
                    {
                        count++;
                    }
                }
                return await Task.FromResult(count);
            }
            catch
            {
                return await Task.FromResult(count);
            }
        }

        public override async Task<bool> DeleteAsync(Guid input)
        {
            try
            {
                var _result = _Db_Context.Questions.Find(input);
                _Db_Context.Questions.Remove(_result);
                _Db_Context.SaveChanges();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                var _result = _Db_Context.Questions.Find(input);
                _result.MediaId = null;
                _result.Answers = null;
                _result.IsDelete = true;
                await this.UpdateAsync(_result);
                return await Task.FromResult(true);
            }
        }

        public override async Task<Question> GetbyIdAsync(Guid input)
        {
            var _result = _Db_Context.Questions.Find(input);
            return await Task.FromResult(_result);
        }

        public override async Task<List<Question>> GetListAsync(SearchQuestionDto input)
        {
            if (input.SubjectId != null && input.type != null && !input.type.Contains("tuvung"))
            {
                Func<Question, bool> wheres = x =>
                      ((!string.IsNullOrEmpty(input.NameDe)) ? x.NameDe.ToLower().Contains(input.NameDe.ToLower()) : true)
                      && ((!string.IsNullOrEmpty(input.NameVi)) ? x.NameVi.ToLower().Contains(input.NameVi.ToLower()) : true)
                      && ((!string.IsNullOrEmpty(input.CommunicationId.ToString())) ? x.CommunicationId.ToString().ToLower().Contains(input.CommunicationId.ToString().ToLower()) : true)
                      && ((!string.IsNullOrEmpty(input.SubjectId.ToString())) ? x.Word.SubjectId == input.SubjectId : true)
                      && ((!string.IsNullOrEmpty(input.QuestionTypeId.ToString())) ? x.QuestionTypeId == input.QuestionTypeId : true)
                      && ((!string.IsNullOrEmpty(input.FormatQuestion.ToString())) ? x.FormatQuestion == input.FormatQuestion : true)
                      && ((!string.IsNullOrEmpty(input.Grammar_TheoryId.ToString())) ? x.Grammar_TheoryId == input.Grammar_TheoryId : true)
                      && ((!string.IsNullOrEmpty(input.WordId.ToString())) ? x.WordId.ToString().ToLower().Contains(input.WordId.ToString().ToLower()) : true)
                      && (x.IsDelete != true)
                      && ((!string.IsNullOrEmpty(input.Status.ToString())) ? x.Status == input.Status : true);

                var _results = _Db_Context.Questions.Where(a => a.WordId != null).Where(wheres).ToList();
                return await Task.FromResult(_results);
            }
            if (input.SubjectId != null && input.type.Contains("tuvung"))
            {
                Func<Question, bool> wheres = x =>
                //      ((!string.IsNullOrEmpty(input.SubjectId.ToString())) ? x.Answers.Where(a => a.IsTrue == true).FirstOrDefault().Word.SubjectId == input.SubjectId : true)
                       ((!string.IsNullOrEmpty(input.QuestionTypeId.ToString())) ? x.QuestionTypeId == input.QuestionTypeId : true)
                      && ((!string.IsNullOrEmpty(input.Status.ToString())) ? x.Status == input.Status : true)
                      && (x.IsDelete != true);
                var _results = _Db_Context.Questions.Where(a => a.WordId != null).Where(wheres).ToList().Where(a => a.Answers != null && a.Answers.Where(b => b.IsTrue == true).FirstOrDefault().Word.SubjectId == input.SubjectId).ToList();
                return await Task.FromResult(_results);
            }
            Func<Question, bool> where = x =>
                       ((!string.IsNullOrEmpty(input.NameDe)) ? x.NameDe.ToLower().Contains(input.NameDe.ToLower()) : true)
                       && ((!string.IsNullOrEmpty(input.NameVi)) ? x.NameVi.ToLower().Contains(input.NameVi.ToLower()) : true)
                       && ((!string.IsNullOrEmpty(input.CommunicationId.ToString())) ? x.CommunicationId.ToString().ToLower().Contains(input.CommunicationId.ToString().ToLower()) : true)
                       && ((!string.IsNullOrEmpty(input.SubjectId.ToString())) ? x.Word.SubjectId == input.SubjectId : true)
                       && ((!string.IsNullOrEmpty(input.QuestionTypeId.ToString())) ? x.QuestionTypeId == input.QuestionTypeId : true)
                       && ((!string.IsNullOrEmpty(input.FormatQuestion.ToString())) ? x.FormatQuestion == input.FormatQuestion : true)
                       && ((!string.IsNullOrEmpty(input.Grammar_TheoryId.ToString())) ? x.Grammar_TheoryId == input.Grammar_TheoryId : true)
                       && ((!string.IsNullOrEmpty(input.Status.ToString())) ? x.Status == input.Status : true)
                       && ((!string.IsNullOrEmpty(input.WordId.ToString())) ? x.WordId.ToString().ToLower().Contains(input.WordId.ToString().ToLower()) : true)
                       && (x.IsDelete != true);
            var _result = _Db_Context.Questions.Where(where).ToList();
            return await Task.FromResult(_result);
        }

        public async Task<List<VocabularyResult>> GetVocabularyResult(SearchAnswerDto input)
        {
            if (input.Type == "result")
            {
                Func<VocabularyResult, bool> where = x =>
         ((!string.IsNullOrEmpty(input.StudenId.ToString())) ? x.StudenId.ToString().ToLower().Contains(input.StudenId.ToString().ToLower()) : true);
                var _result = _Db_Context.VocabularyResults.Where(where).ToList();
                return await Task.FromResult(_result);
            }
            else
            {
                Func<VocabularyResult, bool> where = x =>
          ((!string.IsNullOrEmpty(input.QuestionId.ToString())) ? x.QuestionId.ToString().ToLower().Contains(input.QuestionId.ToString().ToLower()) : true)
          && ((!string.IsNullOrEmpty(input.StudenId.ToString())) ? x.StudenId.ToString().ToLower().Contains(input.StudenId.ToString().ToLower()) : true)
          && ((!string.IsNullOrEmpty(input.WordId.ToString())) ? x.WordAnswerId.ToString().ToLower().Contains(input.WordId.ToString().ToLower()) : true)
          && ((!string.IsNullOrEmpty(input.Status.ToString())) ? x.Status.ToString().ToLower().Contains(input.Status.ToString().ToLower()) : true);
                var _result = _Db_Context.VocabularyResults.Where(where).ToList();
                return await Task.FromResult(_result);
            }

        }

        public override async Task<bool> UpdateAsync(Question input)
        {
            try
            {
                _Db_Context.Entry(input).State = EntityState.Modified;
                _Db_Context.SaveChanges();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> UpdateVocabularyResult(VocabularyResult input)
        {
            try
            {
                var local = _Db_Context.Set<VocabularyResult>()
                         .Local
                         .FirstOrDefault(f => f.Id == input.Id);
                if (local != null)
                {
                    _Db_Context.Entry(local).State = EntityState.Detached;
                }
                _Db_Context.Entry(input).State = EntityState.Modified;

                _Db_Context.SaveChanges();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return await Task.FromResult(false);
            }
        }
    }
}
