import asyncio
import aiohttp

async def fetch(session, url):
    async with session.get(url) as response:
        return await response.text()  
async def fetch_urls(urls: list):
    async with aiohttp.ClientSession() as session:
        tasks = [fetch(session, url) for url in urls] 
        results = await asyncio.gather(*tasks)  
        return results


urls = [
    "https://example.com",
    "https://jsonplaceholder.typicode.com/todos/1",
    "https://jsonplaceholder.typicode.com/users"
]

async def main():
    contents = await fetch_urls(urls)
    for i, content in enumerate(contents, 1):
        print(f"Conteúdo da URL {i}:\n{content[:200]}...\n")  
asyncio.run(main())
