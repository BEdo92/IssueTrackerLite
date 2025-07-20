import { Component } from '@angular/core';
import { inject } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-nav',
  imports: [FormsModule, RouterLink],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  accountService = inject(AccountService);
  private router = inject(Router);
  loginModel: any = {};

  logout() {
    this.accountService.logout();
  }

  login() {
    console.log('Attempting to log in with:', this.loginModel);
    this.accountService.login(this.loginModel).subscribe({
      next: () => {},
      error: (err) => {
      console.error('Login error:', err);
      }
    });
    this.router.navigateByUrl('/');
  }
}
