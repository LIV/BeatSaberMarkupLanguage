﻿using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BeatSaberMarkupLanguage.TypeHandlers
{
    [ComponentHandler(typeof(TextMeshProUGUI))]
    public class TextMeshProUGUIHandler : TypeHandler<TextMeshProUGUI>
    {

        public override Dictionary<string, string[]> Props => new Dictionary<string, string[]>()
        {
            //{ "font", new[]{ "font" } },
            { "text", new[]{"text"} },
            { "fontSize", new[]{"font-size"} },
            { "color", new[]{ "font-color", "color" } },
            { "faceColor", new[]{ "face-color" } },
            { "outlineColor", new[]{ "outline-color" } }, // Outline not supported for Teko fonts
            { "outlineWidth", new[]{ "outline-width" } },
            { "richText", new[]{ "rich-text" } }, // Enabled by default
            { "alignment", new[]{ "font-align","align" } },
            { "overflowMode", new[]{ "overflow-mode" } },
            { "wordWrapping", new[]{ "word-wrapping" } },
            { "bold", new[]{ "bold" } },
            { "italics", new[]{ "italics" } },
            { "underlined", new[]{ "underlined" } },
            { "strikethrough", new[]{ "strikethrough" } },
            { "allUppercase", new[]{ "all-uppercase" } }
        };
        public override Dictionary<string, Action<TextMeshProUGUI, string>> Setters => new Dictionary<string, Action<TextMeshProUGUI, string>>()
        {
            //{"font", new Action<TextMeshProUGUI,string>(SetFont) },
            {"text", new Action<TextMeshProUGUI,string>((textMesh, value) => textMesh.text = value) },
            {"fontSize", new Action<TextMeshProUGUI,string>((textMesh, value) => textMesh.fontSize = Parse.Float(value)) },
            {"color", new Action<TextMeshProUGUI, string>((textMesh, value) => textMesh.color = GetColor(value)) },
            {"faceColor", new Action<TextMeshProUGUI, string>((textMesh, value) => textMesh.faceColor = GetColor(value)) },
            {"outlineColor", new Action<TextMeshProUGUI, string>((textMesh, value) => textMesh.outlineColor = GetColor(value)) },
            {"outlineWidth", new Action<TextMeshProUGUI, string>((textMesh, value) => textMesh.outlineWidth = Parse.Float(value)) },
            {"richText", new Action<TextMeshProUGUI, string>((textMesh, value) => textMesh.richText = Parse.Bool(value)) },
            {"alignment", new Action<TextMeshProUGUI,string>((textMesh, value) => textMesh.alignment = (TextAlignmentOptions)Enum.Parse(typeof(TextAlignmentOptions), value)) },
            {"overflowMode", new Action<TextMeshProUGUI,string>((textMesh, value) => textMesh.overflowMode = (TextOverflowModes)Enum.Parse(typeof(TextOverflowModes), value)) },
            {"wordWrapping", new Action<TextMeshProUGUI,string>((textMesh, value) => textMesh.enableWordWrapping = Parse.Bool(value)) },
            {"bold", new Action<TextMeshProUGUI, string>((textMesh, value) => textMesh.fontStyle = SetStyle(textMesh.fontStyle, FontStyles.Bold, value)) },
            {"italics", new Action<TextMeshProUGUI,string>((textMesh, value) => textMesh.fontStyle = SetStyle(textMesh.fontStyle, FontStyles.Italic, value))  },
            {"underlined", new Action<TextMeshProUGUI,string>((textMesh, value) => textMesh.fontStyle = SetStyle(textMesh.fontStyle, FontStyles.Underline, value))  },
            {"strikethrough", new Action<TextMeshProUGUI,string>((textMesh, value) => textMesh.fontStyle = SetStyle(textMesh.fontStyle, FontStyles.Strikethrough, value))  },
            {"allUppercase", new Action<TextMeshProUGUI,string>((textMesh, value) => textMesh.fontStyle = SetStyle(textMesh.fontStyle, FontStyles.UpperCase, value))  },
        };

        private static FontStyles SetStyle(FontStyles existing, FontStyles modifyStyle, string flag)
        {
            if (Parse.Bool(flag))
                return existing |= modifyStyle;
            else
                return existing &= ~modifyStyle;
        }
        /* TODO: Fix fonts
         * Also add table of safe font names
         * 
        private static void SetFont(TextMeshProUGUI textMesh, string fontName)
        {
            TMP_FontAsset fontAsset = Resources.FindObjectsOfTypeAll<TMP_FontAsset>().Where(t =>
                string.Equals(t.name, fontName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if (fontAsset != null)
            {
                textMesh.gameObject.SetActive(false);
                MonoBehaviour.Destroy(textMesh.font);
                // TODO: Setting the font doesn't apply colors to the new font, but this doesn't work
                //
                color = textMesh.color;
                //var faceColor = textMesh.faceColor;
                //var outlineColor = textMesh.outlineColor;
                //var outlineWidth = textMesh.outlineWidth;
                //textMesh.color = Color.white;
                //textMesh.faceColor = Color.white;
                //textMesh.outlineColor = Color.white;
                //textMesh.outlineWidth = 0;
                textMesh.font = MonoBehaviour.Instantiate(fontAsset);
                //textMesh.ForceMeshUpdate();
                //textMesh.color = color;
                //textMesh.faceColor = faceColor;
                //textMesh.outlineColor = outlineColor;
                //textMesh.outlineWidth = outlineWidth;
                textMesh.gameObject.SetActive(true);
            }
            else
            {
                var fontList = Resources.FindObjectsOfTypeAll<TMP_FontAsset>();
                Logger.log?.Warn($"Font {fontName} not found. Available fonts: {string.Join(", ", fontList?.Where(f => !f.name.Contains("(Clone)")).Select(f => f.name))}");
            }
        }*/

        private static Color GetColor(string colorStr)
        {
            if (ColorUtility.TryParseHtmlString(colorStr, out Color color))
                return color;
            Logger.log?.Warn($"Color {colorStr}, is not a valid color.");
            return Color.white;
        }
    }
}
