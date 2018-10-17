import { Component, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'question-edit',
    templateUrl: './question-edit.component.html',
    styleUrls: ['./question-edit.component.css']
})
export class QuestionEditComponent {
    title: string;
    question: Question;

    editmode: boolean;

    constructor(private activatedRoute: ActivatedRoute,
        private router: Router,
        private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string) {

        this.question = <Question>{};
        var id = +this.activatedRoute.snapshot.params['id'];

        // check if we're in edit mode or not
        this.editmode = (this.activatedRoute.snapshot.url[1].path === 'edit');
        if (this.editmode) {
            // fetch the quiz from the server
            var url = this.baseUrl + "api/question/" + id;
            this.http.get<Question>(url)
                .subscribe(res => {
                    this.question = res;
                    this.title = 'Edit - ' + this.question.Text;
                }, error => console.log(error));
        } else {
            this.question.QuizId = id;
            this.title = 'Create a new question';
        }
    }

    onSubmit(question: Question) {
        var id = +this.activatedRoute.snapshot.params["id"];
        var urlEdit = this.baseUrl + "api/question/" + id;
        var urlAdd = this.baseUrl + "api/question";

        if (this.editmode) {
            this.http.put<Question>(urlEdit, question)
                .subscribe(res => {
                    var v = res;
                    console.log('Question ' + v.Id + ' has been updated.');
                    this.router.navigate(['quiz/edit', v.QuizId]);
                }, error => console.log(error));
        } else {
            this.http.post<Question>(urlAdd, this.question)
                .subscribe(res => {
                    var v = res;
                    console.log('Question ' + v.Id + ' has been created.');
                    this.router.navigate(["quiz/edit", v.QuizId]);
                }, error => console.log(error));
        }
    }

    onBack() {
        this.router.navigate(["quiz/edit", this.question.QuizId])
    }
}