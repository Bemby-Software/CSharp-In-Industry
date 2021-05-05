import { ApiHelperService } from './api-helper.service';
import {Observable} from "rxjs";
import {HttpErrorResponse, HttpResponse, HttpResponseBase} from "@angular/common/http";
import {IApiError, SimpleResult} from "../models/errors";

describe('ApiHelperService', () => {
  let service: ApiHelperService;
  let response: HttpResponseBase;

  beforeEach(() => {
    service = new ApiHelperService();
  });


  it('handleSimpleHttpResponse - should return success result when no error', () => {
    //Arrange
    response = new HttpResponse();
    const response$ = new Observable(sub => {
      sub.next(response)
      sub.complete();
    })

    //Act
    service.handleSimpleHttpResponse(response$)
      .subscribe(got => {

        //Assert
        expect(got.successful).toBeTruthy();
        expect(got.error).toBe("");
      });
    });

  it('handleSimpleHttpResponse - should return something went wrong error if 400 is not IApiError', () => {
    //Arrange
    response = new HttpErrorResponse({
      error: {
        test: 'some bad test value'
      },
      status: 400
    });

    const response$ = createErrorResponseObservable(response);

    //Act
    service.handleSimpleHttpResponse(response$)
      .subscribe(got => {
        //Assert
        expect(got.successful).toBeFalse();
        expect(got.error).toBe(SimpleResult.Failure().error)
      })
  })



  it('handleSimpleHttpResponse - should return api error if error is 400 and is IApiError and is user friendly', () => {

    const error: IApiError = {
      code: '',
      reason: 'error reason',
      isUserFriendly: true
    }

    //Arrange
    response = new HttpErrorResponse({
      error: error,
      status: 400
    });

    const response$ = createErrorResponseObservable(response);

    //Act
    service.handleSimpleHttpResponse(response$)
      .subscribe(got => {
        //Assert
        expect(got.successful).toBeFalse();
        expect(got.error).toBe(error.reason)
      })
  });

  it('handleSimpleHttpResponse - should return something went wrong when error is 400 & IApiError & not user freindly', () => {

    const error: IApiError = {
      code: '',
      reason: 'error reason',
      isUserFriendly: false
    }

    //Arrange
    response = new HttpErrorResponse({
      error: error,
      status: 400
    });

    const response$ = createErrorResponseObservable(response);

    //Act
    service.handleSimpleHttpResponse(response$)
      .subscribe(got => {
        //Assert
        expect(got.successful).toBeFalse();
        expect(got.error).toBe(SimpleResult.Failure().error);
      })
  });

  it('handleDataHttpResponse - should return something went wrong error if 400 is not IApiError', () => {
    //Arrange
    response = new HttpErrorResponse({
      error: {
        test: 'some bad test value'
      },
      status: 400
    });

    const response$ = createErrorResponseObservable(response);

    //Act
    service.handleDataHttpResponse(response$)
      .subscribe(got => {
        //Assert
        expect(got.successful).toBeFalse();
        expect(got.error).toBe(SimpleResult.Failure().error)
      })
  });

  it('handleDataHttpResponse - should return api error if error is 400 and is IApiError and is user friendly', () => {

    const error: IApiError = {
      code: '',
      reason: 'error reason',
      isUserFriendly: true
    }

    //Arrange
    response = new HttpErrorResponse({
      error: error,
      status: 400
    });

    const response$ = createErrorResponseObservable(response);

    //Act
    service.handleDataHttpResponse(response$)
      .subscribe(got => {
        //Assert
        expect(got.successful).toBeFalse();
        expect(got.error).toBe(error.reason)
      })
  });

  it('handleDataHttpResponse - should return something went wrong when error is 400 & IApiError & not user freindly', () => {

    const error: IApiError = {
      code: '',
      reason: 'error reason',
      isUserFriendly: false
    }

    //Arrange
    response = new HttpErrorResponse({
      error: error,
      status: 400
    });

    const response$ = createErrorResponseObservable(response);

    //Act
    service.handleDataHttpResponse(response$)
      .subscribe(got => {
        //Assert
        expect(got.successful).toBeFalse();
        expect(got.error).toBe(SimpleResult.Failure().error);
      })
  });

  it('handleDataHttpResponse - should return data when no error', () => {

    interface Type {
      id: number;
      name: string;
    }

    //Arrange
    const instance: Type = {
      id: 1,
      name: 'Test'
    }

    const response$ = new Observable<Type>(sub => {
      sub.next(instance);
      sub.complete();
    })

    //Act
    service.handleDataHttpResponse<Type>(response$)
      .subscribe(got => {
        //Assert
        expect(got.successful).toBeTrue();
        expect(got.error).toBe("");
        if(got.data) {
          expect(got.data).toEqual(instance);
        }
      })
  });



  const createErrorResponseObservable = (response: HttpResponseBase) => {
    return new Observable(sub => {
      sub.error(response);
      sub.complete();
    })
  }
});


