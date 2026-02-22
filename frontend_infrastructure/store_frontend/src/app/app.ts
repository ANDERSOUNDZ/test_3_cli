import { Component, signal } from '@angular/core';
import { NavComponent } from './core/components/nav/nav.component';

@Component({
  selector: 'app-root',
  imports: [NavComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('store_frontend');
}
