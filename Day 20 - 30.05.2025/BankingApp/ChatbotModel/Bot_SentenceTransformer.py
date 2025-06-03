import pandas as pd
from sentence_transformers import SentenceTransformer, util
from fastapi import FastAPI, Request
import uvicorn

app = FastAPI()

df = pd.read_csv("/Users/presidio/Library/CloudStorage/OneDrive-PresidioNetworkSolutions/Documents/GensParkTraning/Day 19 - 29.05.2025/BankingApp/ChatbotModel/BankFAQs.csv")  # columns: Question, Answer

model = SentenceTransformer('distilbert-base-uncased')

faq_questions = df['Question'].tolist()
faq_answers = df['Answer'].tolist()
faq_embeddings = model.encode(faq_questions, convert_to_tensor=True)

@app.post("/ask")
async def ask(request: Request):
    data = await request.json()
    user_question = data.get("question")
    if not user_question:
        return {"error": "No question provided"}

    user_embedding = model.encode(user_question, convert_to_tensor=True)

    # Compute cosine similarity
    scores = util.cos_sim(user_embedding, faq_embeddings)[0]
    top_idx = scores.argmax().item()

    # Get best match
    best_question = faq_questions[top_idx]
    best_answer = faq_answers[top_idx]
    confidence = scores[top_idx].item()

    return {
        "user_question": user_question,
        "matched_faq": best_question,
        "answer": best_answer,
        "similarity_score": round(confidence, 2)
    }

if __name__ == "__main__":
    uvicorn.run(app, host="0.0.0.0", port=8000)
