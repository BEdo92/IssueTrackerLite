import { Component, inject } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  username: string = '';
  email: string = '';
  password: string = '';
  confirmPassword: string = '';
  registrationSuccess: boolean = false;
  registrationError: string = '';

  private accountService = inject(AccountService);

  register() {
    console.log('Registering user - RegisterComponent');
    
    if (this.password !== this.confirmPassword) {
      this.registrationError = 'Passwords do not match.';
      this.registrationSuccess = false;
      return;
    }

    this.accountService.register({
      username: this.username,
      email: this.email,
      password: this.password
    }).subscribe({
      next: () => {
        this.registrationSuccess = true;
        this.registrationError = '';
        this.username = '';
        this.email = '';
        this.password = '';
        this.confirmPassword = '';
      },
      error: (err) => {
        this.registrationSuccess = false;
        this.registrationError = err?.error?.message || 'Registration failed.';
      }
    });
  }
}
