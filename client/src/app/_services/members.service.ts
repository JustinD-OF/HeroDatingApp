import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of, take } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';
import { User } from '../_models/user';
import { UserParameters } from '../_models/userParameters';
import { AccountService } from './account.service';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;
  members: Member[] = [];
  memberCache = new Map();
  user: User | undefined;
  userParameters: UserParameters | undefined;

  constructor(private http: HttpClient, private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if (user) {
          this.userParameters = new UserParameters(user);
          this.user = user;
        }
      }
    })
  }

  getUserParameters(){
    return this.userParameters;
  }

  setUserParameters(parameters : UserParameters){
    this.userParameters =parameters
  }

  resetUserParameters(){
    if (this.user) {
      this.userParameters = new UserParameters(this.user);
      return this.userParameters;
    }
    return;
  }

  getMembers(userParams: UserParameters){

    const response = this.memberCache.get(Object.values(userParams).join('-'));

    if (response)
      return of(response);

    let params = getPaginationHeaders(userParams.pageNumber, userParams.pageSize);

    params = params.append('minAge', userParams.minAge);
    params = params.append('maxAge', userParams.maxAge);
    params = params.append('gender', userParams.gender);
    params = params.append('orderBy', userParams.orderBy);

    return getPaginatedResult<Member[]>(this.baseUrl + 'users/', params, this.http).pipe(
      map(response => {
        this.memberCache.set(Object.values(userParams).join('-'), response);
        return response;
      })
    )
  }

  addLike(userName: string){
    return this.http.post(this.baseUrl + 'likes/' + userName, {});
  }

  getLikes(predicate: string, pageNumber: number, pageSize: number){
    let params = getPaginationHeaders(pageNumber, pageSize);

    params = params.append('predicate', predicate);

    return getPaginatedResult<Member[]>(this.baseUrl + 'likes', params, this.http)
  }

  getMember(userName: string){
    const member = [...this.memberCache.values()]
      .reduce((array, element) => array.concat(element.result), [])
      .find((member: Member) => member.userName === userName);

    if (member)
      return of(member);

    return this.http.get<Member>(this.baseUrl + 'users/' + userName);
  }

  updateMember(member: Member) {
    return this.http.put(this.baseUrl + 'users/', member).pipe(
      map(() => {
        const index = this.members.indexOf(member);
        this.members[index] = {...this.members[index], ...member}
      })
    );
  }

  deletePhoto(photoId: number){
    return this.http.delete(this.baseUrl + 'users/delete-photo/' + photoId);
  }

  setMainPhoto(photoId: number){
    return this.http.put(this.baseUrl + 'users/set-main-photo/' + photoId, {});
  }
}
