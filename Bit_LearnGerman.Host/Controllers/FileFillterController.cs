﻿using DE_APPLICATION_ELEANING.CategoryApp;
using DE_APPLICATION_ELEANING.MediaApp;
using DE_APPLICATION_ELEANING.SubjectApp;
using DE_APPLICATION_ELEANING.TemplateApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Oauth_2._0_v2.Controllers
{
    public class FileFillterController : Controller
    {
        private readonly CategoryAppLication _categoryApp;
        private readonly SubjectApplication _subjectApp;
        private readonly MediaApplication  _mediaApp;
        private readonly TemplateApplication _templateApp;

        public FileFillterController()
        {
            if (_categoryApp == null)
            {
                _categoryApp = new CategoryAppLication();
            }
            if (_subjectApp == null)
            {
                _subjectApp = new SubjectApplication();
            }
            if (_mediaApp == null)
            {
                _mediaApp = new MediaApplication();
            }
            if (_templateApp == null)
            {
                _templateApp = new TemplateApplication();
            }
        }
        // GET: FileUrl
        public async Task<ActionResult> Index(string sub, string Id)
        {
            string baseFile = "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";
            string defaultFormat = "image/png";
            switch (sub)
            {
                case "typesubject":
                    var filetypeStr = await _categoryApp.GetbyIdAsync(Guid.Parse(Id));
                    if (filetypeStr.Icon != "" && filetypeStr.Icon != null)
                    {
                        baseFile = filetypeStr.Icon;
                    }
                    break;
                case "subject":
                    var fileSubStr = await _subjectApp.GetbyIdAsync(Guid.Parse(Id));
                    if (fileSubStr.Icon != "" && fileSubStr.Icon != null)
                    {
                        baseFile = fileSubStr.Icon;
                    }
                    break;
                case "mediaquery":
                    var questionvocabulary = await _mediaApp.GetbyIdAsync(Guid.Parse(Id));
                    if (questionvocabulary != null && questionvocabulary.Media != "" && questionvocabulary.Media != null)
                    {
                        baseFile = questionvocabulary.Media;
                        defaultFormat = questionvocabulary.Format;
                    }
                    break; 
            }
            byte[] fileContent = Convert.FromBase64String(baseFile);
            return File(fileContent, defaultFormat);

        }
        public async Task<ActionResult> GetTemplate(Guid Id, string en = "vi")
        {
            ViewBag.en = en;
            var _result = await _templateApp.GetbyIdAsync(Id);
            return View(_result);
        }
    }
}