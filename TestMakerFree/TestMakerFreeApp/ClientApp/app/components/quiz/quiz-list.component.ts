import { Component, Inject, Input, OnInit } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Router } from '@angular/router';

@Component({
    selector: 'quiz-list',
    templateUrl: './quiz-list.component.html',
    styleUrls: ['./quiz-list.component.css']
})
export class QuizListComponent implements OnInit {

    @Input() class: String;
    title: String;
    selectedQuiz: Quiz;
    quizzes: Quiz[];
  
    constructor(private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: String,
        private router: Router) {
    }

    onSelect(quiz: Quiz) {
        this.selectedQuiz = quiz;
        console.log('quiz with id ', this.selectedQuiz.Id, ' has been selected');
        this.router.navigate(["quiz", this.selectedQuiz.Id]);
    }

    ngOnInit(): void {
        var url = this.baseUrl + "/api/quiz/";
        switch (this.class) {
            case 'latest':
            default:
                this.title = 'Latest Quizzes';
                url += 'latest/10';
                break;
            case 'byTitle':
                this.title = 'Quizzes By Title';
                url += 'bytitle/10';
                break;
            case 'random':
                this.title = 'Random Quizzes';
                url += 'random/10';
                break;
        }

        this.http.get<Quiz[]>(url).subscribe(result => {
            this.quizzes = result;
        }, error => console.error(error));
    }
}