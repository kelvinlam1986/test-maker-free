import { Component, Input, Inject, OnChanges, SimpleChanges } from '@angular/core'
import { HttpClient } from '@angular/common/http'
import { Router } from '@angular/router'

@Component({
    selector: 'answer-list',
    templateUrl: './answer-list.component.html',
    styleUrls: ['./answer-list.component.css']
})
export class AnswerListComponent implements OnChanges {
    @Input() question: Question;
    answers: Answer[];
    title: string;

    constructor(private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string,
        private router: Router) {

        this.answers = [];
    }

    ngOnChanges(changes: SimpleChanges): void {
        if (typeof changes['question'] !== "undefined") {
            // retrieve the question variable info change
            var change = changes['question'];

            // only perform the task if the value has been changed
            if (!change.isFirstChange()) {
                this.loadData();
            }
        }
    }

    loadData() {
        var url = this.baseUrl + "api/answer/All/" + this.question.Id;
        this.http.get<Answer[]>(url)
            .subscribe(res => {
                this.answers = res;
            }, error => console.log(error));
    }

    onCreate() {
        this.router.navigate(['/answer/create', this.question.Id]);
    }

    onEdit(answer: Answer) {
        this.router.navigate(['answer/edit', answer.Id]);
    }

    onDelete(answer: Answer) {
        if (confirm('Do you really want to delete this answer ?')) {
            var url = this.baseUrl + "api/answer/" + answer.Id;
            this.http.delete(url)
                .subscribe(res => {
                    console.log('Question ' + answer.Id + ' has been deleted');
                    // refresh the answer list
                    this.loadData();
                }, error => console.log(error));
        }
    }
}