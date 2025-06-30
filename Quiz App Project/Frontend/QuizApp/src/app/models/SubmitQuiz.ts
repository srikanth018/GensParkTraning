export class SubmitQuiz{
    constructor(
        public quizId:string ='',
        public studentEmail:string='',
        public questions:QuestionsArray[] = [],
        public startedAt:string='',
        public endedAt:string=''
    ){}
}
export class QuestionsArray{
    constructor(
        public questionId:string ='',
        public selectedOptionIds:string[]=[],
    ){}
}