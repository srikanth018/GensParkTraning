import pandas as pd
from transformers import AutoTokenizer, AutoModelForCausalLM
import torch
from fastapi import FastAPI, Request
import uvicorn
import difflib

app = FastAPI()


df = pd.read_csv("/Users/presidio/Library/CloudStorage/OneDrive-PresidioNetworkSolutions/Documents/GensParkTraning/Day 19 - 29.05.2025/BankingApp/ChatbotModel/BankFAQs.csv")   


model_name = "TinyLlama/TinyLlama-1.1B-Chat-v1.0"
cache_dir = "./local_tinyllama_model"

tokenizer = AutoTokenizer.from_pretrained(model_name, cache_dir=cache_dir)
model = AutoModelForCausalLM.from_pretrained(model_name, cache_dir=cache_dir)

device = "cuda" if torch.cuda.is_available() else "cpu"
model = model.to(device)

# Get top 3 FAQs
def get_top_faqs(user_question, top_n=3):
    questions = df['Question'].tolist()
    scores = [(q, difflib.SequenceMatcher(None, user_question.lower(), q.lower()).ratio()) for q in questions]
    sorted_scores = sorted(scores, key=lambda x: x[1], reverse=True)[:top_n]
    return [q for q, _ in sorted_scores]

# Build context
def build_context(user_question):
    faqs = []
    for q in get_top_faqs(user_question):
        a = df[df['Question'] == q]['Answer'].values[0]
        faqs.append(f"Q: {q}\nA: {a}")
    return "\n\n".join(faqs)

# Generate answer
def generate_answer(user_question):
    filtered_context = build_context(user_question)
    prompt = f"You are a helpful banking assistant. Use the following FAQs to answer the user's question.\n\n{filtered_context}\n\nUser Question: {user_question}\nAnswer:"
    
    inputs = tokenizer(prompt, return_tensors="pt", truncation=True, max_length=2048).to(device)
    outputs = model.generate(**inputs, max_new_tokens=100, pad_token_id=tokenizer.eos_token_id)
    
    answer = tokenizer.decode(outputs[0], skip_special_tokens=True)
    return answer.split("Answer:")[-1].strip()

@app.post("/ask")
async def ask(request: Request):
    data = await request.json()
    question = data.get("question")
    if not question:
        return {"error": "No question provided"}
    try:
        answer = generate_answer(question)
        return {"answer": answer}
    except Exception as e:
        return {"error": str(e)}

if __name__ == "__main__":
    uvicorn.run(app, host="0.0.0.0", port=8000)
