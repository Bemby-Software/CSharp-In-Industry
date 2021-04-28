export interface IApiError {
    code: string;
    reason: string;
    isUserFriendly: boolean
}

export interface IResult {
  error: string;
  successful: boolean;
}

export interface IDataResult<T> extends IResult {
  data: T | null;
}


export class DataResult<T> implements IDataResult<T> {
  data: T | null = null;
  error: string;
  successful: boolean;

  constructor(error: string, successful: boolean, data: T | null = null) {
    this.error = error;
    this.successful = successful;
    this.data = data;
  }

  static Failure<T>(error: string | null = null) {
    return new DataResult<T>(error === null ? "Oops! Something went wrong." : error, false);
  }

  static Success<T>(data: T) {
    return new DataResult("", true, data);
  }

}

export class SimpleResult implements IResult {
  error: string;
  successful: boolean;

  constructor(successful: boolean, error: string) {
    this.error = error;
    this.successful = successful;
  }

  static Success() {
    return new SimpleResult(true, "");
  }

  static Failure(error: string | null = null) {
    return new SimpleResult(false, error === null ? "Oops! Something went wrong." : error);
  }
}
