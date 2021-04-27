import { HttpClient } from '@angular/common/http';
import { fakeAsync, flush } from '@angular/core/testing';
import { Subject } from 'rxjs';
import { SimpleResult, ValidationService } from './validation.service';

describe('AppComponent', () => {
    let validationService: ValidationService;
    let httpClientSpy: jasmine.SpyObj<HttpClient>;

  beforeEach(async () => {
    httpClientSpy = jasmine.createSpyObj<HttpClient>('HttpClient', ['post']);
    validationService = new ValidationService(httpClientSpy);
  });

  fit('should transform it correctly', fakeAsync(() => {
    // Arrange
    const expectedResult = 'result';
    let result: SimpleResult = {} as SimpleResult;

    const post$ = new Subject<{ statusCode: number, body: string }>();
    httpClientSpy.post.and.returnValue(post$.asObservable());

    validationService.example(['test']).subscribe((res) => {
        result = res;
    });

    // Act
    post$.next({
        statusCode: 200,
        body: expectedResult
    });
    flush();

    // Assert
    expect(result).toBeTruthy();
    expect(result.result).toBe(expectedResult);
    expect(result.success).toBeTrue();

  }));
});
