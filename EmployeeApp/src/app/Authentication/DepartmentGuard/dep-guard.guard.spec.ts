import { TestBed } from '@angular/core/testing';

import { DepGuardGuard } from './dep-guard.guard';

describe('DepGuardGuard', () => {
  let guard: DepGuardGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(DepGuardGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
