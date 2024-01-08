import {Component, Input} from '@angular/core';
import {Chef} from "../../../swagger";
import {ChefService} from "../../services";

@Component({
  selector: 'app-chef-card',
  templateUrl: './chef-card.component.html',
  styleUrl: './chef-card.component.css'
})
export class ChefCardComponent {
  @Input() chef !: Chef;

  constructor(protected chefService: ChefService) {}
}
