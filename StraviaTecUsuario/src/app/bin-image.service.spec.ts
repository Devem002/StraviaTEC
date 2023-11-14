import { TestBed } from '@angular/core/testing';

import { BinImageService } from './bin-image.service';

describe('BinImageService', () => {
  let service: BinImageService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BinImageService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
