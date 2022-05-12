import http from "./Http-comman";
class OverlappingScheduleService {

    getAll() {
        return http.get("Meetings/GetMeetingsWithOverlaps");
    }

}
export default new OverlappingScheduleService();