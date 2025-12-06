# BlurX

**BlurX** is a lightweight, enterprise-grade **data masking library for .NET** that allows you to protect sensitive information (PII, credentials, emails, phone numbers, card numbers, etc.) directly at the model/DTO property level using simple attributes.

BlurX is built for:
- ✅ Zero-effort masking
- ✅ Fine-grained field control
- ✅ Multiple masking styles
- ✅ High performance reflection engine
- ✅ Enterprise-ready extensibility

---

## ✨ Features

- Mask individual fields using attributes
- Control how many characters stay visible
- Support for multiple masking strategies:
  - **Prefix**
  - **Suffix**
  - **Middle**
  - **Full masking**
  - **Email masking**
  - **Regex masking**
- Custom:
  - Mask character (e.g. `*`, `#`)
  - Visible character count
  - Blur (mask) character count
- Works on:
  - Single objects
  - Lists & collections

---

## 📦 Installation

Install from NuGet:

```bash
dotnet add package BlurX
