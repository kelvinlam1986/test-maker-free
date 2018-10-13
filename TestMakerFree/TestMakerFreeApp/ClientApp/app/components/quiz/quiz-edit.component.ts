import { Component, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'quiz-edit',
    templateUrl: './quiz-edit.component.html',
    styleUrls: ['./quiz-edit.component.css']
})
export class QuizEditComponent {
    title: string;
    quiz: Quiz;

    editMode: boolean;

    constructor(private activatedRoute: ActivatedRoute,
        private router: Router,
        private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string) {

        // create empty object from the Quiz interface;
        this.quiz = <Quiz>{};

        var id = +this.activatedRoute.snapshot.params["id"];
        if (id) {
            this.editMode = true;
            var url = this.baseUrl + "api/quiz/" + id;
            this.http.get<Quiz>(url).subscribe(res => {
                this.quiz = res;
                this.title = "Edit - " + this.quiz.Title;
            }, error => console.log(error));
        } else {
            this.editMode = false;
            this.title = "Create a new Quiz";
        }
    }

    onBack() {
        this.router.navigate(["home"]);
    }

    onSubmit(quiz: Quiz) {
        var id = +this.activatedRoute.snapshot.params["id"];
        var urlEdit = this.baseUrl + "api/quiz/" + id;
        var urlAdd = this.baseUrl + "api/quiz";
        if (this.editMode) {
            this.http
                .put<Quiz>(urlEdit, quiz)
                .subscribe(res => {
                    var v = res;
                    console.log("Quiz " + v.Id + " has been updated.");
                    this.router.navigate(["home"]);
                }, error => console.log(error));
        } else {
            console.log(quiz);
            this.http
                .post<Quiz>(urlAdd,  quiz)
                .subscribe(res => {
                    var q = res;
                    console.log("Quiz " + q.Id + " has been created.");
                    this.router.navigate(["home"]);
                }, error => console.log(error));
        }
    }
}