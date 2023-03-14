import { Component, OnInit } from '@angular/core';
import { Observable, take } from 'rxjs';
import { Member } from 'src/app/_models/member';
import { Pagination } from 'src/app/_models/pagination';
import { User } from 'src/app/_models/user';
import { UserParameters } from 'src/app/_models/userParameters';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit{
  // members$: Observable<Member[]> | undefined;

  members: Member[] = [];
  pagination: Pagination | undefined;
  userParameters: UserParameters | undefined;
  user: User | undefined;
  genderList = [{ value: 'male', display: 'Males' }, { value: 'female', display: 'Females'}];

  constructor(private memberService: MembersService, private accountService: AccountService){
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if (user) {
          this.userParameters = new UserParameters(user);
          this.user = user;
        }
      }
    })
  }

  ngOnInit(): void {
    this.loadMembers();
  }

  pageChanged(event: any){
    if (this.userParameters && this.userParameters?.pageNumber !== event.page){
      this.userParameters.pageNumber = event.page;
      this.loadMembers();
    }
  }

  loadMembers() {
    if (!this.userParameters) return;
    this.memberService.getMembers(this.userParameters).subscribe({
      next: response => {
        if (response.result && response.pagination){
          this.members = response.result;
          this.pagination = response.pagination;
        }
      }
    })
  }

  resetFilter(){
    if (this.user){
      this.userParameters = new UserParameters(this.user);
      this.loadMembers();
    }
  }

}
