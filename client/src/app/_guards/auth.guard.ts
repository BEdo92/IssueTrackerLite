import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';

export const authGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService); 

  if (accountService.currentUser()) {
    return true;
  } else {
    //toastr.error('You shall not pass!'); // TODO: Implement toastr
    return false;
  }
};
