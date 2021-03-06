﻿using System.IO;
using System.Linq;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Module.CodeGenerator.Infrastructure.Templates.Models;

namespace NetModular.Module.CodeGenerator.Infrastructure.Templates.Default.T4.src.UI.App.src.views.index
{
    public partial class Index : ITemplateHandler
    {
        private readonly TemplateBuildModel _model;
        private ClassBuildModel _class;
        private readonly string _uiPrefix;

        public Index(TemplateBuildModel model)
        {
            _model = model;
            _uiPrefix = _model.Module.UiPrefix.ToLower();
        }

        public bool IsGlobal => false;

        public void Save()
        {
            if (_model.Module.ClassList != null && _model.Module.ClassList.Any())
            {
                foreach (var classModel in _model.Module.ClassList)
                {
                    _class = classModel;

                    var dir = Path.Combine(_model.RootPath, $"src/UI/{_model.Module.WebUIDicName}/src/views", _class.Name.FirstCharToLower(), "index");
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);

                    //清空
                    GenerationEnvironment.Clear();

                    var content = TransformText();

                    var filePath = Path.Combine(dir, "index.vue");
                    File.WriteAllText(filePath, content);
                }
            }
        }
    }
}
