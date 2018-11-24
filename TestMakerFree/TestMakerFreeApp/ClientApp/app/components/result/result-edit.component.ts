import { Component, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'result-edit',
    templateUrl: './result-edit.component.html',
    styleUrls: ['./result-edit.component.css']
})
export class ResultEditComponent {
    title: string;
    result: Result;

    editmode: boolean;

    constructor(private activatedRoute: ActivatedRoute,
        private router: Router,
        private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string) {

        this.result = <Result>{};
        var id = +this.activatedRoute.snapshot.params['id'];

        // check if we're in edit mode or not
        this.editmode = (this.activatedRoute.snapshot.url[1].path === 'edit');
        if (this.editmode) {
            // fetch the quiz from the server
            var url = this.baseUrl + "api/result/" + id;
            this.http.get<Result>(url)
                .subscribe(res => {
                    this.result = res;
                    this.title = 'Edit - ' + this.result.Text;
                }, error => console.log(error));
        } else {
            this.result.QuizId = id;
            this.title = 'Create a new result';
        }
    }

    onSubmit(result: Result) {
        var id = +this.activatedRoute.snapshot.params["id"];
        var urlEdit = this.baseUrl + "api/result/" + id;
        var urlAdd = this.baseUrl + "api/result";

        if (this.editmode) {
            this.http.put<Result>(urlEdit, result)
                .subscribe(res => {
                    var v = res;
                    console.log('Result ' + v.Id + ' has been updated.');
                    this.router.navigate(['quiz/edit', v.QuizId]);
                }, error => console.log(error));
        } else {
            this.http.post<Result>(urlAdd, this.result)
                .subscribe(res => {
                    var v = res;
                    console.log('Result ' + v.Id + ' has been created.');
                    this.router.navigate(["quiz/edit", v.QuizId]);
                }, error => console.log(error));
        }
    }

    onBack() {
        this.router.navigate(["quiz/edit", this.result.QuizId])
    }
}