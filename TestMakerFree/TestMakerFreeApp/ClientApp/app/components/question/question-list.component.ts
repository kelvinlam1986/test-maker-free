﻿import { Component, OnChanges, SimpleChanges, Input, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'question-list',
    templateUrl: './question-list.component.html',
    styleUrls: ['./question-list.component.css']
})
export class QuestionListComponent implements OnChanges {

    @Input() quiz: Quiz;
    questions: Question[];
    title: string;

    constructor(private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string,
        private router: Router) {
        this.questions = [];
    }

    ngOnChanges(changes: SimpleChanges): void {
        if (typeof changes['quiz'] !== "undefined") {
            // retrieve the quiz variable info change
            var change = changes['quiz'];

            // only perform the task if the value has been changed
            if (!change.isFirstChange()) {
                this.loadData();
            }
        }
    }

    loadData() {
        var url = this.baseUrl + "api/question/All/" + this.quiz.Id;
        this.http.get<Question[]>(url)
            .subscribe(res => {
                this.questions = res;
            }, error => console.log(error));
    }

    onCreate() {
        this.router.navigate(['/question/create', this.quiz.Id]);
    }

    onEdit(question: Question) {
        this.router.navigate(['/question/edit', question.Id]);
    }

    onDelete(question: Question) {
        if (confirm('Do you really want to delete this question ?')) {
            var url = this.baseUrl + "api/question/" + question.Id;
            this.http.delete(url)
                .subscribe(res => {
                    console.log('Question ' + question.Id + ' has been deleted');
                    // refresh the question list
                    this.loadData();
                }, error => console.log(error));
        }
    }

}