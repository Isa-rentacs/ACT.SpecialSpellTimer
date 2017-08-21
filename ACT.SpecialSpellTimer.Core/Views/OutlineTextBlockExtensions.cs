﻿using System.Windows;

using ACT.SpecialSpellTimer.Config;
using FFXIV.Framework.Common;

namespace ACT.SpecialSpellTimer.Views
{
    public static class OutlineTextBlockExtensions
    {
        internal static FontInfo GetFontInfo(
            this OutlineTextBlock control)
        {
            return new FontInfo(
                control.FontFamily,
                control.FontSize,
                control.FontStyle,
                control.FontWeight,
                control.FontStretch);
        }

        internal static void SetFontInfo(
            this OutlineTextBlock control,
            FontInfo fontInfo)
        {
            if (control.GetFontInfo().ToString() != fontInfo.ToString())
            {
                control.FontFamily = fontInfo.Family;
                control.FontSize = fontInfo.Size;
                control.FontStyle = fontInfo.Style;
                control.FontWeight = fontInfo.Weight;
                control.FontStretch = fontInfo.Stretch;
            }
        }

        /// <summary>
        /// 自動計算したStrokeThicknessを設定する
        /// </summary>
        /// <param name="t">OutlineTextBlock</param>
        public static void SetAutoStrokeThickness(
            this OutlineTextBlock t)
        {
            // 基準の太さ
            var thickness = 1.0d;

            // フォントサイズを基準に補正をかける
            thickness *=
                t.FontSize / 11.0d;

            // ウェイトによる補正をかける
            thickness *= (
                t.FontWeight.ToOpenTypeWeight() /
                FontWeights.Normal.ToOpenTypeWeight())
                * 0.9d;

            // 設定によって増幅させる
            var textOutlineThicknessGain = 1.0d;
            if (!WPFHelper.IsDesignMode)
            {
                textOutlineThicknessGain = Settings.Default.TextOutlineThicknessRate;
            }

            var newThickness = thickness * textOutlineThicknessGain;

            if (t.StrokeThickness != newThickness)
            {
                t.StrokeThickness = newThickness;
            }
        }
    }
}