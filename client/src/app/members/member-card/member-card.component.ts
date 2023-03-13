import { Component, Input } from '@angular/core';
import { faEnvelope, faHeart, faUser } from '@fortawesome/free-solid-svg-icons';
import { Member } from 'src/app/_models/member';

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


}
