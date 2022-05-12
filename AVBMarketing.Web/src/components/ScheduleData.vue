<template>
    <container_tag>
        <div class="row" style="margin-bottom: 10px;">
            <div class="col-sm-12 btn btn-success">
                Schedules
            </div>
        </div>
        <div>
            <table class="table">
                <thead>
                <th>Id</th>
                <th>Start Date</th>
                <th>End Date</th>
                <th></th>
                </thead>
                <tbody>
                    <tr v-for="item in schedules" :key="item.Id">
                        <td>{{item.Id}}</td>
                        <td>{{format_date(item.StartDate) }}</td>
                        <td>{{format_date(item.EndDate) }}</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </container_tag>
</template>  
  
<script>  
    import ScheduleService from "../ScheduleService";
    import moment from 'moment';

    export default {
        name: 'ScheduleList',
        data()
        {
            return {
                schedules: [],
            };
        },
        created()
        {
            this.RetrieveSchedules();
        },

        methods: {
            RetrieveSchedules() {
                ScheduleService.getAll().then(response => {
                    this.schedules = response.data;
                    console.log(response.data);
                })
                    .catch(e => {
                        console.log(e);
                    });
            },

            format_date(value) {
                if (value) {
                    return moment(String(value)).format("MMM ddd, yyyy hh:ss")
                }
            }
        }
    }
</script>  
  
<style lang="scss" scoped>  
  
</style>  