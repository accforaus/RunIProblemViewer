using System;
using Domain.Problem.Data;
using UnityEngine;

namespace ProblemViewer.Scripts.Problem {
    [Serializable]
    public class Problem {
        [SerializeField] public string _id;
        [SerializeField] public string school;
        [SerializeField] public string grade;
        [SerializeField] public string unit;
        [SerializeField] public string type;
        [SerializeField] public ProblemData data;
        
        public void Base64ToSprite() {
            data.Base64ToSprite();
        }

        public bool IsExampleEmpty => data.example.IsEmpty;
        public bool IsExampleBoxEmpty => data.exampleBox.IsEmpty;
        public bool IsMultipleEmpty => data.multiple.IsEmpty;
    }
}