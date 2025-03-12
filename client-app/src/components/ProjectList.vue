<template>
    <div>
        <h2>Project List</h2>
        <ul>
            <li v-for="project in projects" :key="project.projectId">
                <router-link :to="'/projects/' + project.projectId">{{ project.projectName }}</router-link>
            </li>
        </ul>
        <router-link to="/projects/create">Create New Project</router-link>
    </div>
</template>
