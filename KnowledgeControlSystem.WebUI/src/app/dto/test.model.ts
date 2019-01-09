import { Question } from './question.model';

export class Test{
        public Id:number;
        public Name:string;
        public Duration:number;
        public CategoryId:number;
        public Questions: Question[];

        constructor(){}
}