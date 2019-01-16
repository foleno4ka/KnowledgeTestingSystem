import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { TestService } from '../api/service/test.service';
import { FormArray, FormBuilder, FormGroup, Validators, AbstractControl, FormControl } from '@angular/forms';
import { Test } from '../api/dto/test.model';
import { Category } from '../api/dto/category.model';
import 'rxjs/add/operator/map';
import { CategoryService } from '../api/service/category.service';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/filter';
import { HttpClient } from '@angular/common/http';
import { Question } from '../api/dto/question.model';
import { from } from 'rxjs';
import { formArrayNameProvider } from '@angular/forms/src/directives/reactive_directives/form_group_name';
import { analyzeAndValidateNgModules } from '@angular/compiler';

export enum QUESTION_TYPE {
    SINGLE = "SINGLE", MULTIPLE = "MULTIPLE"
}
@Component({
    selector: 'test-page',
    templateUrl: './test-page.component.html'
})
export class TestPageComponent implements OnInit {
    isNew: boolean;
    workingTest: Test;
    isCreatingError: boolean = false;
    categories: Category[];
    response: string;
    questionTypes: string[] = [QUESTION_TYPE.SINGLE, QUESTION_TYPE.MULTIPLE];
    constructor(private testService: TestService,
        private categoryService: CategoryService,
        private router: Router,
        private route: ActivatedRoute) { }

    ngOnInit(): void {
        const id: string = this.route.snapshot.params['id'];
        this.isNew = (id === "new");
        if (this.isNew) {
            this.workingTest = {
                Id: undefined,
                CategoryId: undefined,
                Duration: undefined,
                Name: undefined,
                Questions: [this.createDefaultQuestion()]
            };
        } else {
            this.workingTest = {} as any;
            this.testService.getTestById(id).subscribe((data: Test) => {
                this.workingTest = data as Test;
            });
        }

        this.categoryService.getCategories().subscribe(data => {
            this.categories = data;
        });
    }

    createDefaultQuestion(): Question {
        return {
            Id: undefined,
            Type: QUESTION_TYPE.SINGLE,
            Score: 1,
            Text: undefined,
            TestId: undefined,
            Answers: [this.createDefaultAnswer()]
        };
    }

    createDefaultAnswer() {
        return {
            Id: undefined,
            QestionId: undefined,
            Correct: false,
            Text: undefined
        };
    }

    isCorrectChanged($event: any, questionId: number, answerId: number) {
        const question = this.workingTest.Questions[questionId];
        if (question.Type !== QUESTION_TYPE.SINGLE) {
            return;
        }
        if ($event.currentTarget.checked !== true) {
            return;
        }
        const answers = question.Answers;
        for (let i = 0; i < answers.length; i++) {
            if (i !== answerId) {
                answers[i].Correct = false;
            }
        }
    }

    addQuestion(): void {
        this.workingTest.Questions.push(this.createDefaultQuestion());
    }

    addAnswer(questionId): void {
        this.workingTest.Questions[questionId].Answers.push(this.createDefaultAnswer());
    }

    save(): void {
        const testObservable = this.isNew
            ? this.testService.createTest(this.workingTest)
            : this.testService.updateTest(this.workingTest.Id, this.workingTest);
        testObservable.subscribe(res => {
            this.router.navigate(["/tests"]);
        });
    }
}
