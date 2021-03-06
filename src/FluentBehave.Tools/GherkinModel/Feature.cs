﻿using System.Collections.Generic;

namespace FluentBehave.Tools.GherkinModel
{
    public class Feature
    {
        public Feature()
        {
            ScenarioCollection = new List<Scenario>();
        }

        public string Text { get; set; }
        public IList<Scenario> ScenarioCollection { get; set; }
    }
}
