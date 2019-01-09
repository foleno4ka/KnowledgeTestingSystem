import { Answer } from './answer.model';

export class Question {
        public Id:number;
        public TestId:number;
        public Text:string;
        public Score:number;
        public Type:string;
        public Answers:Answer[];

        constructor(){}
}