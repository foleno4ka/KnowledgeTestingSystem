export class TestResult{
    public Id:number;
    public TestId:number;
    public TestName:string;
    public UserId:number;
    public Duration: string;
    public Score:number;
    public TotalScore:number;
    public StartTime:Date;
    public EndTime:string;
    public PassTime:string;
    public MaxDuration:number;
    constructor(Duration:string, EndTIme:Date ){}
}