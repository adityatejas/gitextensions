﻿using System;
using System.Drawing;
using System.IO;
using FluentAssertions;
using GitCommands;
using GitExtUtils.GitUI.Theming;
using GitUI.Theming;
using NUnit.Framework;

namespace GitUITests.Theming
{
    [TestFixture]
    public class AppColorDefaultsTests
    {
        [Test]
        public void Default_values_are_defined_in_AppColorDefaults()
        {
            foreach (AppColor name in Enum.GetValues(typeof(AppColor)))
            {
                Color value = AppColorDefaults.GetBy(name);
                value.Should().NotBe(AppColorDefaults.FallbackColor);
            }
        }

        [Test]
        public void Default_values_are_specified_in_invariant_theme()
        {
            var testAccessor = AppSettings.GetTestAccessor();
            string applicationExecutablePath = testAccessor.ApplicationExecutablePath;

            try
            {
                testAccessor.ApplicationExecutablePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "gitextensions.exe");

                var controller = new FormThemeEditorController(null, new ThemePersistence());
                var invariantTheme = controller.LoadInvariantTheme(quiet: true);
                foreach (AppColor name in Enum.GetValues(typeof(AppColor)))
                {
                    Color value = invariantTheme.GetColor(name);
                    value.Should().NotBe(Color.Empty);
                }
            }
            finally
            {
                testAccessor.ApplicationExecutablePath = applicationExecutablePath;
            }
        }
    }
}
