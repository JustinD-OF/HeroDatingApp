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
export class MemberListComponent implements OnInit {
  // members$: Observable<Member[]> | undefined;

  members: Member[] = [];
  pagination: Pagination | undefined;
  userParameters: UserParameters | undefined;
  genderList = [{ value: 'male', display: 'Males' }, { value: 'female', display: 'Females' }];

  constructor(private memberService: MembersService) {
    this.userParameters = memberService.getUserParameters();
  }

  ngOnInit(): void {
    this.loadMembers();
  }

  pageChanged(event: any) {
    if (this.userParameters && this.userParameters?.pageNumber !== event.page) {
      this.userParameters.pageNumber = event.page;
      this.memberService.setUserParameters(this.userParameters);
      this.loadMembers();
    }
  }

  loadMembers() {
    if (this.userParameters) {
      this.memberService.setUserParameters(this.userParameters);
      this.memberService.getMembers(this.userParameters).subscribe({
        next: response => {
          if (response.result && response.pagination) {
            this.members = response.result;
            this.pagination = response.pagination;
          }
        }
      })
    }
  }

  resetFilter() {
    this.userParameters = this.memberService.resetUserParameters();
    this.loadMembers();
  }

}
