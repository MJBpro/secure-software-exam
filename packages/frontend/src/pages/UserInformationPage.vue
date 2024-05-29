<template>
<div>

<div class="w-[32rem] bg-slate-200 rounded p-6">
        <h1 class="text-2xl my-3 font-medium">Hello, please insert your information here</h1>

        <div class="flex gap-4 text-left flex-col p-4">

            <div>
                <label class="block text-gray-600 text-sm font-bold mb-2" for="fullname">
                  First name
                </label>
                <input class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" id="fullname" type="text" placeholder="Casper">
            </div>

              <div>
                <label class="block text-gray-600 text-sm font-bold mb-2" for="lastName">
                  Last name
                </label>
                <input class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" id="lastName" type="text" placeholder="Jahn">
            </div>
            <div>
                <label class="block text-gray-600 text-sm font-bold mb-2" for="address">
                  Address
                </label>
                <input class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" id="address" type="text" placeholder="Amagerbrogade 12">
            </div>
            <div>
                <label class="block text-gray-600 text-sm font-bold mb-2" for="address">
                  Birthdate
                </label>
                <input class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" id="address" type="date" placeholder="Amagerbrogade 12">
            </div>
          
        </div>

        <div>
          <button class="bg-blue-400 p-2 rounded text-white font-medium px-4 mt-2">Upload information</button>
        </div>

  </div>
    
</div>
    
</template>
<script setup>

</script>