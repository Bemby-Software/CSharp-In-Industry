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
  data?: T;
  asResult(): IResult;
}


export class DataResult<T> implements IDataResult<T> {
  data?: T;
  error: string;
  successful: boolean;

  constructor(error: string, successful: boolean, data?: T) {
    this.error = error;
    this.successful = successful;
    this.data = data;
  }

  public asResult() {
    return new SimpleResult(this.successful, this.error);
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

  static Success() : IResult {
    return new SimpleResult(true, "");
  }

  static Failure(error: string | null = null) : IResult {
    return new SimpleResult(false, error === null ? "Oops! Something went wrong." : error);
  }
}
