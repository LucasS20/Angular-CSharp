import { TestBed } from '@angular/core/testing';

import { ReService } from './re.service';

describe('ReService', () => {
  let service: ReService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ReService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
