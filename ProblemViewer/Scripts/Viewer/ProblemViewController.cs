using System;
using System.Collections;
using System.Linq;
using ProblemViewer.Scripts.Viewer.View;
using UnityEngine;

namespace ProblemViewer.Scripts.Viewer {
    public enum MultipleLocation {
        Question, Multiple
    }
    
    public class MultipleImage {
        public MultipleLocation multipleLocation;
    }
    public class ProblemViewController : MonoBehaviour {
        [SerializeField] private ProblemViewQuestion questionController;
        [SerializeField] private ProblemViewExample exampleController;
        [SerializeField] private ProblemViewExample exampleBoxController;
        [SerializeField] private ProblemViewMultiple multipleController;

        [SerializeField] private GameObject examplePanel;
        [SerializeField] private GameObject exampleBoxPanel;
        [SerializeField] private GameObject multiplePanel;
        [SerializeField] public CanvasGroup canvas; 
        
        public void Create(Problem.Problem problem, bool tutorial = false) {
            CheckMultipleMultiImage(problem);
            CheckMultipleOneImage(problem);
            InactiveViews(problem);
            questionController.Create(problem.data.question, tutorial ? 28.0f : 48.0f);
            if (!problem.IsExampleEmpty)
                exampleController.Create(problem.data.example, tutorial ? 20.0f : 38.0f);
            if (!problem.IsExampleBoxEmpty)
                exampleBoxController.Create(problem.data.exampleBox, tutorial ? 20.0f : 38.0f);
            if (!problem.IsMultipleEmpty)
                multipleController.Create(problem.data.multiple, tutorial ? 20.0f : 38.0f);
        }

        private void InactiveViews(Problem.Problem problem) {
            if (problem.data.example.IsEmpty) examplePanel.SetActive(false);
            if (problem.data.exampleBox.IsEmpty) exampleBoxPanel.SetActive(false);
            if (problem.data.multiple.IsEmpty)  multiplePanel.SetActive(false);
        }

        private void CheckMultipleOneImage(Problem.Problem problem) {
            MultipleImage multipleImage = null;
            var multipleIsEmpty = problem.data.multiple.IsEmpty;
            var multiple = problem.data.multiple;
            var multipleHasOneImage = multiple.grids == null && multiple.table == null && multiple.content == "" &&
                                      multiple.images.Count == 1;
            if (multipleIsEmpty || multipleHasOneImage) {
                multipleImage = multipleIsEmpty ? new MultipleImage { multipleLocation = MultipleLocation.Question} : new MultipleImage { multipleLocation = MultipleLocation.Multiple};
                if (multipleIsEmpty && problem.data.multiple.images.Count > 0) {
                    problem.data.multiple.images.Add(problem.data.question.images.Last());
                    problem.data.question.images.RemoveAt(problem.data.question.images.Count - 1);
                }
            }

            questionController.multipleImage = multipleImage;
            multipleController.multipleImage = multipleImage;
        }

        private void CheckMultipleMultiImage(Problem.Problem problem) {
            var multipleImageCount = problem.data.multiple.images.Count;
            if (multipleImageCount > 1) {
                for (int index = 1; index < multipleImageCount; index++) 
                    problem.data.question.images.Add(problem.data.multiple.images[index]);
                for (int index = 1; index < multipleImageCount; index++)
                    problem.data.multiple.images.RemoveAt(problem.data.multiple.images.Count - 1);
            }
        }
    }
}