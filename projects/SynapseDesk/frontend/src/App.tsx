import { useState } from 'react'

export default function App() {
  const [message, setMessage] = useState('Создай тикет: не работает оплата')
  const [reply, setReply] = useState('')

  async function send() {
    const res = await fetch('/api/agent/chat', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ message })
    })
    const data = await res.json()
    setReply(data.reply)
  }

  return (
    <main style={{ fontFamily: 'system-ui', maxWidth: 640, margin: '2rem auto' }}>
      <h1>SynapseDesk</h1>
      <textarea value={message} onChange={e => setMessage(e.target.value)} rows={4} style={{ width: '100%' }} />
      <button onClick={send}>Спросить агента</button>
      {reply && <p><b>Ответ:</b> {reply}</p>}
    </main>
  )
}
