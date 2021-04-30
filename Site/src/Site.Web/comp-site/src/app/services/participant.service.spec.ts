import { ParticipantService } from './participant.service';

describe('Participants Service', () => {
  let service: ParticipantService;

  beforeEach(() => {
    service = new ParticipantService({} as any, {} as any);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
