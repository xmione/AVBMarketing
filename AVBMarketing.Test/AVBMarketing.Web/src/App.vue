<template>
    <container_tag>
        <div id="app">
            <Home msg="AVB Marketing Meeting Schedules" />
            <ScheduleList />
            <OverlappingScheduleList />
        </div>
    </container_tag>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    import Home from './components/Home.vue';
    import ScheduleList from './components/ScheduleData.vue';
    import OverlappingScheduleList from './components/OverlappingScheduleData.vue';

    @Component({
        components: {
            Home,
            ScheduleList,
            OverlappingScheduleList
        }
    })
    export default class App extends Vue { }
</script>

<style>
    #app {
        font-family: Avenir, Helvetica, Arial, sans-serif;
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
        text-align: center;
        color: #2c3e50;
        margin-top: 10px;
    }
</style>     