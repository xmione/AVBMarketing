import http from "./Http-comman";
class ScheduleService {

    getAll() {
        return http.get("Meetings");
    }

}
export default new ScheduleService();