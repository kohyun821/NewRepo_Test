<template>
  <div>
    <table v-if="materials.length">
      <thead>
        <tr>
          <th>ID</th>
          <th>Name</th>
          <th>Amount</th>
          <th>Total</th>
          <th>Status</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="material in materials" :key="material.materialId">
          <td>{{ material.materialId }}</td>
          <td>{{ material.materialName }}</td>
          <td>{{ material.materialAmount }}</td>
          <td>{{ material.materialTotal }}</td>
          <td>{{ material.materialStatus ? 'Available' : 'Not Available' }}</td>
        </tr>
      </tbody>
    </table>
    <div v-else>
      <p>No materials found.</p>
    </div>
  </div>
</template>

<script>
import axios from 'axios'

export default {
  data() {
    return {
      materials: [],
    }
  },
  async created() {
    try {
      const response = await axios.get('https://localhost:7059/Material')
      console.log(response.data);
      this.materials = response.data
    } catch (error) {
      console.error(error)
    }
  },
}
</script>
