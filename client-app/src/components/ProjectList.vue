<template>
    <div>
        <h2>Project List</h2>
        <ul>
            <li v-for="project in projects" :key="project.projectId">
                <router-link :to="'/projects/' + project.projectId">
                    {{ project.name }}
                </router-link>
            </li>
        </ul>
        <router-link to="/projects/create">Create New Project</router-link>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        data() {
            return {
                projects: [],
            };
        },
        mounted() {
            this.fetchProjects();
        },
        methods: {
            async fetchProjects() {
                try {
                    const response = await axios.get('/api/projects'); 
                    this.projects = response.data; 
                } catch (error) {
                    console.error('Ошибка при загрузке проектов:', error);
                }
            },
        },
    };
</script>