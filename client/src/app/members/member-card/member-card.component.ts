import { Component, Input } from '@angular/core';
import { faEnvelope, faHeart, faUser, faGlobe } from '@fortawesome/free-solid-svg-icons';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';
import { ToastrService } from 'ngx-toastr';
import { PresenceService } from 'src/app/_services/presence.service';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent {
  @Input() member: Member | undefined;
  faUser = faUser;
  faEnvelope = faEnvelope;
  faHeart = faHeart;
  faGlobe = faGlobe;


  constructor(private memberService: MembersService, private toastr: ToastrService, public presenceService: PresenceService){}

  ngOnInit() : void {

  }
  addLike(member: Member){
    this.memberService.addLike(member.userName).subscribe({
      next: () => this.toastr.success('You liked ' + member.knownAs)
    });

  }
}
