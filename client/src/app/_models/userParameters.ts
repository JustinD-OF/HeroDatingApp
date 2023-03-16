import { User } from "./user";

export class UserParameters{
  gender: string;
  minAge = 18;
  maxAge = 100;
  pageNumber = 1;
  pageSize = 5;
  orderBy = 'lastActive';
  

  constructor(user: User){
    this.gender = user.gender === 'female' ? 'male' : 'female'
  }
}
