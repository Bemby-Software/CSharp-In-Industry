import { of } from 'rxjs';
import { DataResult, SimpleResult } from '../models/errors';
import { ITeamParticipant } from '../models/participant';
import { ParticipantService } from './participant.service';

import { UserSessionService } from './user-session.service';

describe('UserSessionService', () => {
  let service: UserSessionService;
  let participantsServiceSpy: jasmine.SpyObj<ParticipantService>
  let teamParticipant: ITeamParticipant = {
      id: 1,
      forename: "Joe",
      surname: "Bloggs",
      email: "joe.bloggs@hotmail.com",
      team: {
        id: 2,
        name: "test team",
        participants: []
      }
  }

  beforeEach(() => {
    participantsServiceSpy = jasmine.createSpyObj<ParticipantService>('participantService', ['signIn', 'getParticipantDetails']);
    service = new UserSessionService(participantsServiceSpy as ParticipantService);
  });

  describe('signIn', () => {

    let error: string = "sample error message";
    const email = "test@test.com";
    const token = "token"


    it('should return unsuccesful result if sign in fails', () => {

      //Arrange
      var result$ = of(SimpleResult.Failure(error));


      participantsServiceSpy.signIn.withArgs(email, token).and.returnValue(result$);

      //Act
      service.signIn(email, token).subscribe(final => {
        //Assert
        expect(final.successful).toBeFalse();
        expect(final.error).toBe(error);
      });
    });


    it('should return succseful result if gets participants details', () => {

      //Arrange
      var result$ = of(SimpleResult.Success());
      var participantResult$ = of(DataResult.Success<ITeamParticipant>(teamParticipant))


      participantsServiceSpy.signIn.withArgs(email, token).and.returnValue(result$);
      participantsServiceSpy.getParticipantDetails.withArgs(token).and.returnValue(participantResult$);

      //Act
      service.signIn(email, token).subscribe(final => {
        //Assert
        expect(final.successful).toBeTrue();
        expect(final.error).toBe('');
        expect(service.getParticipant()).toEqual(teamParticipant);
      });
    });

    it('should return succseful result if gets participants details fails', () => {

      //Arrange
      var result$ = of(SimpleResult.Success());
      var participantResult$ = of(DataResult.Failure<ITeamParticipant>(error))


      participantsServiceSpy.signIn.withArgs(email, token).and.returnValue(result$);
      participantsServiceSpy.getParticipantDetails.withArgs(token).and.returnValue(participantResult$);

      //Act
      service.signIn(email, token).subscribe(final => {
        //Assert
        expect(final.successful).toBeFalse();
        expect(final.error).toBe(error);
      });
    });

  });
});
