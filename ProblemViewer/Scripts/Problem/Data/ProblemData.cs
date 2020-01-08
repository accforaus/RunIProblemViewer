using System;
using UnityEngine;

namespace Domain.Problem.Data {
    [Serializable]
    public class ProblemData {
        [SerializeField] public ProblemDataContent question;
        [SerializeField] public ProblemDataContent example;
        [SerializeField] public ProblemDataContent exampleBox;
        [SerializeField] public ProblemDataContent multiple;
        [SerializeField] public ProblemDataContent solution;

        public ProblemData() {
            question = new ProblemDataContent();
            example = new ProblemDataContent();
            exampleBox = new ProblemDataContent();
            multiple = new ProblemDataContent();
            solution = new ProblemDataContent();
        }

        public ProblemData(ProblemDataContent question, ProblemDataContent example, ProblemDataContent exampleBox, ProblemDataContent multiple, ProblemDataContent solution) {
            this.question = question;
            this.example = example;
            this.exampleBox = exampleBox;
            this.multiple = multiple;
            this.solution = solution;
        }

        public void Base64ToSprite() {
            question.Base64ToSprite();
            example.Base64ToSprite();
            exampleBox.Base64ToSprite();
            multiple.Base64ToSprite();
            solution.Base64ToSprite();
        }
    }
}