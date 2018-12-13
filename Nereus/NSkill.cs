//**************************************
// Author              :    DannyShen
// Email               :    dannyshenl@163.com
// Create Time         :    2016/6/12 15:21:27
// Update Time         :    2016/6/12 15:21:27
// *************************************
// CLR Version         :    4.0.30319.42000
// Class Version       :    v1.0.0.0
// Class Description   :    
// *************************************
// Copyright ©SL 2016 . All rights reserved.
// *************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Nereus
{
    using Power;

    public class NSkill
    {

        private static string _head_html =
            @"<!DOCTYPE html>
                    <html>
                    <head>
                        <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
                        <meta http-equiv='Content-Type' content='text/html; charset=gb2312' />
                        <title>{0}</title>
                        <meta name='keywords' content='info_interface' />
                        <meta name='description' content='info_interface' />
                        <meta content='width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0' name='viewport' />
                        <meta content='yes' name='apple-mobile-web-app-capable' />
                        <meta content='black' name='apple-mobile-web-app-status-bar-style' />
                        <meta content='telephone=no' name='format-detection' />
                        <link href='{1}' rel='stylesheet' />
                        <script src='http://code.jquery.com/jquery-1.11.3.min.js'></script>
                        <script src='{2}'></script>
                    </head>";

        public static StringBuilder BuidlerNDocHTML(string assemblyString, string title = "", string cssPath = "", string jsPath = "")
        {
            title = string.IsNullOrWhiteSpace(title) ? assemblyString + "接口文档" : title;
            cssPath = string.IsNullOrWhiteSpace(cssPath) ? "http://192.168.1.142:1213/content/css/main.css" : cssPath;
            jsPath = string.IsNullOrWhiteSpace(jsPath) ? "http://192.168.1.142:1213/content/js/core.js" : jsPath;
            StringBuilder sber = new StringBuilder(string.Format(_head_html, title, cssPath, jsPath));
            sber.Append(string.Format(@"<body style='background-color:#2e2e2e;'><div style='padding-left:10%;padding-right:10%;'><div class='title_box'><h1>{0}</h1></div>", title));

            var controllers = BuilderNStructure(assemblyString);
            sber.Append("<ul class='cata_2' id='content1'><div style = 'padding-left:24px;padding-right:24px;' >");
            foreach (var controller in controllers)
            {
                sber.Append("<li>");
                sber.Append(string.Format("<p class='title_cata_2'>{0} {1}_<span>{2}</span></p>", controller.Name, controller.Code, controller.Description));
                sber.Append("<ul class='cata_3'>");
                if (controller.Actions == null || controller.Actions.Count <= 0)
                {
                    sber.Append("<li><font color='white'>暂无接口</font></li>");
                }
                else
                {
                    foreach (var action in controller.Actions)
                    {
                        sber.Append("<li>");
                        sber.Append(string.Format("<p class='title_cata_3'>{0}_<span>{1}</span><span style='margin-left:20px'>{2}</span></p>", action.Name, action.Code, action.Description));
                        sber.Append("<div class='info_box'><div style='padding-left:15px;padding-right:15px;'>       <ul>");

                        //接口名
                        sber.Append(string.Format("<li><h2>接口名</h2><div class='contant_box'><div class='content_box_div'><p class='name_interface'>{0}</p></div></div></li>", action.Code));

                        //请求类型
                        sber.Append(string.Format("<li><h2>请求类型</h2><div class='contant_box'><div class='content_box_div'><p class='type_request'>{0}</p></div></div></li>", action.Method));

                        //参数
                        sber.Append("<li><h2>参数</h2><div class='contant_box'><div class='content_box_div'><p class='parameter'>");
                        if (action.ParamType == null && (action == null || action.Params.Count <= 0))
                        {
                            sber.Append("无参数");
                        }
                        else if (action.ParamType != null)
                        {
                            sber.Append(PropertyExps<NParamAttribute>(action.ParamType));
                        }
                        else
                        {
                            for (int i = 0; i < action.Params.Count; i++)
                            {
                                var field = action.Params[i];
                                var exps = (object[])action.ParamExplain;
                                var exp = i >= exps.Length ? "" : exps[i];
                                sber.AppendLineHtml(string.Format("{0}&nbsp;<span class='types'>{1}</span>&nbsp;<font color='#759E75'>{2}</font>", exp, field.ParamType.Name, field.DefaultValue.ToString() == "DBNull" ? "" : "默认值：" + field.DefaultValue.ToString()));
                            }
                        }
                        sber.Append("</p></div></div></li>");

                        //返回解释
                        sber.Append(string.Format("<li><h2>返回解释</h2><div class='contant_box'><div class='content_box_div'><p class='return_explain'>"));
                        if (!string.IsNullOrWhiteSpace(action.ReturnExplain))
                        {
                            sber.Append(action.ReturnExplain);
                        }
                        else
                        {
                            sber.Append("暂无解释");
                        }
                        sber.Append("</p></div></div></li>");

                        //返回数据结构
                        string dataFormat = @"{0} - <font color='#689CA6'>{1}</font>&nbsp;<span class='types'>{2}</span>";
                        sber.Append(string.Format("<li><h2>返回数据结构</h2><div class='contant_box'><div class='content_box_div'><p class='return_data'>{0}</p></div></div></li>", PropertyExps<NReturnAttribute>(action.ReturnType, dataFormat)));

                        //链接
                        if (string.IsNullOrWhiteSpace(action.Route))
                        {
                            string actionUrl = string.Format("{0}{1}{2}", controller.Area != null ? controller.Area.Code : "",
                          "/" + controller.Code, string.IsNullOrWhiteSpace(action.Code) ? "" : "/" + action.Code);
                            sber.Append(string.Format("<li><h2>链接</h2><div class='contant_box'><div class='content_box_div'><p class='link'>{0}</p></div></div></li>", actionUrl));
                        }
                        else
                        {
                            sber.Append(string.Format("<li><h2>链接</h2><div class='contant_box'><div class='content_box_div'><p class='link'>{0}</p></div></div></li>", action.Route));
                        }

                        sber.Append("</ul></div></div>");
                        sber.Append("</li>");
                    }
                }
                sber.Append("</ul>");
                sber.Append("</li>");
            }
            sber.Append("</div></ul></div></body></html>");
            return sber;
        }

        public static List<NController> BuilderNStructure(string assemblyString)
        {
            //Get All NAreaAttributies
            var areaAttrs = Assembly.Load(assemblyString).GetTypes().Where(lm => lm.IsClass && lm.GetNAttribute<NAreaAttribute>() != null);

            List<NController> controllers = new List<NController>();
            //Get All NControllerAttributies
            foreach (var controlAttr in Assembly.Load(assemblyString).GetTypes().Where(lm => lm.IsClass && lm.GetNAttribute<NControllerAttribute>() != null))
            {
                NController controller = controlAttr.GetNAttribute<NControllerAttribute>().Controller;
                //Get All Actions In NController
                foreach (var method in controlAttr.GetMethods())
                {
                    NActionAttribute actionAttr = method.GetNAttribute<NActionAttribute>();
                    if (actionAttr != null && actionAttr.Enable)
                    {

                        if (actionAttr.Action.ParamType == null)
                        {
                            foreach (var item in method.GetParameters())
                            {
                                actionAttr.Action.Params.Add(new NParam()
                                {
                                    Code = item.Name,
                                    DefaultValue = item.DefaultValue.ToString(),
                                    ParamType = item.ParameterType
                                });
                            }
                        }
                        controller.Actions.Add(actionAttr.Action);
                    }
                }

                // Find NArea Of The NController
                foreach (var areaAttr in areaAttrs)
                {
                    if (controlAttr.BaseType == areaAttr)
                    {
                        controller.Area = areaAttr.GetNAttribute<NAreaAttribute>().Area;
                    }
                }
                controllers.Add(controller);
            }
            return controllers;
        }


        public static List<NController> BuildNStructure()
        {
            var asssembly = Assembly.GetExecutingAssembly();
            var controllers = new List<NController>();
            var currentNControllers = asssembly.GetCustomAttributes<NControllerAttribute>();
            foreach (var item in currentNControllers)
            {
                var nControllers = new NController();
                var methods = item.GetType().GetMethods();
                foreach (var method in methods)
                {
                    var actions = method.GetCustomAttributes<NActionAttribute>();
                    foreach (var action in actions)
                    {
                        if (!action.Enable)
                        {
                            continue;
                        }
                        var nAction = new NAction()
                        {
                            Code = action.Code,
                            Description = action.Description,
                            Enable = true,
                            Method = action.Method,
                            Name = action.Name,
                            Params = new List<NParam>()
                            {

                            },

                        };


                    }
                }
            }

            return controllers;
        }

        private static string PropertyExps<T>(Type t, string dataFormat = "{0}&nbsp;<span class='types'>{1}</span>&nbsp;<font color='#759E75'>{2}</font>", int deep = 0) where T : Attribute
        {
            StringBuilder sb = new StringBuilder();
            string spaceFormat = "";
            for (int i = 0; i < deep; i++)
            {
                spaceFormat += "<span>&nbsp;&nbsp;&nbsp;&nbsp;</span>";
            }
            deep++;
            foreach (var prop in t.GetProperties())
            {
                Attribute paramAttr = prop.GetNAttribute<T>();
                if (paramAttr == null)
                {
                    continue;
                }
                //sb.AppendWhitespace(addSpace);
                var nameProperty = paramAttr.GetType().GetProperties().First(lm => lm.Name == "Name");

                string zhCN = nameProperty != null ? nameProperty.GetValue(paramAttr, null).ToString() : "";
                sb.Append(spaceFormat);
                sb.AppendLineHtml(string.Format(dataFormat, prop.Name, zhCN, prop.PropertyType.Name));

                if (prop.PropertyType.IsGenericType)
                {
                    sb.Append(PropertyExps<T>(prop.PropertyType.GetGenericArguments()[0], dataFormat, deep));
                }
                else if (prop.PropertyType.IsClass)
                {
                    sb.Append(PropertyExps<T>(prop.PropertyType, dataFormat, deep));
                }
            }
            return sb.ToString();
        }

        public static List<NClass> BuildNClass()
        {
            return NClass.BuildNClass();
        }
    }
}
