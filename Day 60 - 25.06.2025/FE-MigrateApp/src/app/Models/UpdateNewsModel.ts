export class UpdateNewsModel {
  constructor(
    public userId: number | null,
    public title: string = '',
    public shortDescription: string = '',
    public image: string = '',
    public content: string = '',
    public createdDate: Date | null = null,
    public status: number | null = null
  ) {}
}