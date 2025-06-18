export class UserModel {
    constructor(public id:number=0, public firstName:string = "", public lastName:string="", public gender:string="", public role:string="",public state:string="",) {
    }

    static mapUserData(data:any): UserModel{
        return  new UserModel(
            data.id,
            data.firstName,
            data.lastName,
            data.gender,
            data.role,
            data.address?.state 
        )
    }
}