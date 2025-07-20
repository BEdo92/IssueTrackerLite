import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  imports: [FormsModule, CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  title = 'Welcome to Issue Tracker Lite';
  description = 'Track your issues efficiently and easily.';
  private accountService = inject(AccountService);
  model: any = {};
  showLogin: any;
  private router = inject(Router);

  goToRegister(): void {
    this.router.navigate(['/register']);
  }

  login(): void {
    // Example: simple login logic (replace with real authentication)
    console.log('Attempting to log in with:', this.model);
    this.accountService.login(this.model).subscribe({
      next: (result) => {
      console.log('Login successful', result);
      // You might set a logged-in flag or navigate to a dashboard
      },
      error: (err) => {
      console.log('Invalid credentials', err);
      }
    });
  }

}
