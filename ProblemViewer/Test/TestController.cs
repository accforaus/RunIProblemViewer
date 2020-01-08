using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
using ProblemViewer.Scripts.Problem;
using ProblemViewer.Scripts.Utils;
using ProblemViewer.Scripts.Viewer;
using UnityEngine;
using UnityEngine.UI;


namespace ProblemViewer.Test {
    public class TestController : MonoBehaviour {
        [SerializeField] private GameObject problemPrefab;
        [SerializeField] private GameObject problemContent;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button previousButton;
        [SerializeField] private Text indexText;
        [SerializeField] private ProblemViewController controller;
        [SerializeField] private RectTransform problemContentRect;
        [SerializeField] private bool debug;
        public int startIndex = 247;

        private int currentIndex = -1;
        private int problemIndex = -1;
        private List<ProblemViewController> problemObjects = new List<ProblemViewController>();
        
        private readonly string filename = "problems.json";

        private void Start() {
            if (debug) {
                controller.canvas.Visible(true);
                problemContent.SetActive(false);
                DebugQuestion();
            }
            else {
                SetQuestion();
                nextButton.onClick.AddListener(Next);
                previousButton.onClick.AddListener(Previous);
            }
        }

        private void Next() {
            if (currentIndex + 1 < problemObjects.Count) {
                currentIndex++;
                problemIndex++;
                problemObjects[currentIndex - 1].canvas.Visible(false);
                problemObjects[currentIndex].canvas.Visible(true);
                SetOnIndex();
            }
        }

        private void Previous() {
            if (currentIndex - 1 > -1) {
                currentIndex--;
                problemIndex--;
                problemObjects[currentIndex + 1].canvas.Visible(false);
                problemObjects[currentIndex].canvas.Visible(true);
                SetOnIndex();
            }
        }

        private void SetOnIndex() {
            indexText.text = problemIndex.ToString();
        }

        private void SetQuestion() {
            var path = Path.Combine(System.Environment.CurrentDirectory, filename);
            if (File.Exists(path)) {
                var json = File.ReadAllText(path);
                var questions = JsonConvert.DeserializeObject<List<Problem>>(json);
                questions.ForEach(problem => problem.Base64ToSprite());
                CreateQuestion(questions, startIndex);
            }
        }

        private void DebugQuestion() {
            var path = Path.Combine(System.Environment.CurrentDirectory, filename);
            if (File.Exists(path)) {
                var json = File.ReadAllText(path);
                var questions = JsonConvert.DeserializeObject<List<Problem>>(json);
                var problem = questions[startIndex];
                problem.Base64ToSprite();
                controller.Create(problem);
            }
        }

        private void CreateQuestion(List<Problem> problems, int startIndex) {
            for (int index = startIndex; index < problems.Count; index++) {
                var problem = problems[index];
                var item = Instantiate(problemPrefab, problemContent.transform.position, Quaternion.identity);
                item.transform.SetParent(problemContent.transform);
                var itemRect = item.GetComponent<RectTransform>();
                itemRect.localScale = Vector3.one;
                itemRect.offsetMax = Vector2.zero;
                itemRect.offsetMin = Vector2.zero;
                itemRect.anchorMax = Vector2.one;
                var controller = item.GetComponent<ProblemViewController>();
                controller.Create(problem);
                controller.canvas.Visible(false);
                problemObjects.Add(controller);
            }
            problemObjects.First().canvas.Visible(true);
            currentIndex = 0;
            problemIndex = startIndex;
            SetOnIndex();
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) Previous();
            if (Input.GetKeyDown(KeyCode.RightArrow)) Next();
        }
    }
}